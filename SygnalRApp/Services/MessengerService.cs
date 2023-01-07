using System;
using System.Collections.Generic;
using System.Linq;

using NLog;

using SignalRApp.Models;
using SignalRApp.Models.MessagerModels;
using SignalRApp.Repositories.Interfaces;

namespace SignalRApp.Services
{
    /// <summary>
    /// Сервис для работы с сообщениями
    /// </summary>
    public class MessengerService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public MessengerService(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        /// <summary>
        /// Получаем список сообщений между конкретными пользователями, отсортированный от более поздних сообщений к более ранним
        /// </summary>
        /// <param name="currentUserId">Id текущего пользователя</param>
        /// <param name="recipientUserId">Id получателя</param>
        /// <returns>Список сообщений</returns>
        public ResultDataModel<List<MessageModel>> GetMessagesList(Guid? currentUserId, Guid recipientUserId)
        {
            try
            {
                var currentUser = _userRepository.FindItemByGuid(currentUserId);
                var recipientUser = _userRepository.FindItemByGuid(recipientUserId);

                if (currentUser == null || recipientUser == null)
                {
                    return new ResultDataModel<List<MessageModel>>("Пользователь не найден, попробуйте снова позже.");
                }

                var messages = _messageRepository
                    .GetUsersTred(recipientUserId, currentUserId.Value)
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

        /// <summary>
        /// Получаем список тредов
        /// </summary>
        /// <param name="userId">Id пользователя в системе</param>
        /// <returns></returns>
        public ResultDataModel<List<TredModel>> GetTredsList(Guid? userId)
        {
            var result = new List<TredModel>();
            try
            {
                var currenrUser = _userRepository.FindItemByGuid(userId);

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
