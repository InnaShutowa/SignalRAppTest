using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using SignalRApp.Entities;
using SignalRApp.Models;
using SignalRApp.Models.MessagerModels;
using SignalRApp.Repositories.Interfaces;
using SignalRApp.Services.Interfaces;

namespace SignalRApp.Services
{
    /// <inheritdoc cref="IMessageRepository" />
    public class MessengerService : IMessengerService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public MessengerService(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        /// <inheritdoc/>
        public ResultModel AddMessage(Guid? authorUserId,
            Guid? recipientUserId, 
            string text)
        {
            try
            {
                var authorUser = _userRepository.GetItemByGuid(authorUserId);
                var recipientUser = _userRepository.GetItemByGuid(recipientUserId);

                if (authorUser == null || recipientUser == null)
                {
                    return new ResultModel("Пользователь не найден, попробуйте снова позже.");
                }

                var messages = _messageRepository.AddItem(new MessageEntity
                {
                    AuthorUserId = authorUser.Id,
                    RecipientUserId = recipientUser.Id,
                    IsRead = false,
                    Text = text
                });

                return new ResultModel(true);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}, authorUserId = {authorUserId},  recipientUserId={recipientUserId}");

                return new ResultModel($"Что-то пошло не так, попробуйте снова позже.");
            }
        }

        /// <inheritdoc/>
        public ResultDataModel<List<MessageModel>> GetMessagesList(Guid? currentUserId, Guid? recipientUserId)
        {
            try
            {
                var currentUser = _userRepository.GetItemByGuid(currentUserId);
                var recipientUser = _userRepository.GetItemByGuid(recipientUserId);

                if (currentUser == null || recipientUser == null)
                {
                    return new ResultDataModel<List<MessageModel>>("Пользователь не найден, попробуйте снова позже.");
                }

                var messages = _messageRepository
                    .GetUsersTred(recipientUserId.Value, currentUserId.Value)
                    .OrderByDescending(a => a.CreatedAt)
                    .Select(a => new MessageModel
                    {
                        Text = a.Text,
                        SendDate = a.CreatedAt,
                        IsOutgoing = a.AuthorUserId == currentUserId
                    })
                    .ToList();

                return new ResultDataModel<List<MessageModel>>(messages);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}, currentUserId = {currentUserId},  recipientUserId={recipientUserId}");

                return new ResultDataModel<List<MessageModel>>($"Что-то пошло не так, попробуйте снова позже.");
            }
        }

        /// <inheritdoc/>
        public ResultDataModel<List<TredModel>> GetTredsList(Guid? userId)
        {
            var result = new List<TredModel>();
            try
            {
                var currenrUser = _userRepository.GetItemByGuid(userId);

                if (currenrUser == null)
                {
                    return new ResultDataModel<List<TredModel>>("Текущий пользователь не найден, попробуйте снова позже.");
                }

                var users = _userRepository.GetAllUsers();
                if (!users.Any())
                {
                    return new ResultDataModel<List<TredModel>>("Пользователи не найдены, попробуйте снова позже.");
                }

                foreach (var user in users)
                {
                    if (currenrUser.Id == user.Id)
                    {
                        continue;
                    }

                    var messagesCount = _messageRepository.GetUnreadMessages(currenrUser.Id, user.Id);
                    result.Add(new TredModel(user.Id, user.Login, user.JpegPhoto, messagesCount));
                }

                return new ResultDataModel<List<TredModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}, userId = {userId}");

                return new ResultDataModel<List<TredModel>>($"Что-то пошло не так, попробуйте снова позже.");
            }
        }
    }
}
