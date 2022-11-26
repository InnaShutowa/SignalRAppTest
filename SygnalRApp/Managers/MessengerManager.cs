using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using SignalRApp.Interfaces;
using SignalRApp.Models;

namespace SignalRApp.Managers
{
    public class MessengerManager
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public MessengerManager(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public ResultModel<List<TredModel>> GetTredsList(string userName)
        {
            var errMessage = "";
            var result = new List<TredModel>();
            try
            {
                var currenrUser = _userRepository.FindItemByLogin(userName);
                var users = _userRepository.GetAllUsers();
                if (!users.Any())
                {
                    errMessage = "Users weren't found";
                    _logger.Warn(errMessage);
                    return new ResultModel<List<TredModel>>(errMessage);
                }

                users.ForEach(user =>
                {
                    var messages = _messageRepository.GetUnreadMessages(currenrUser.Id, user.Id);
                    result.Add(new TredModel(user.Id, user.Login, user.JpegPhoto, messages.Count));
                });

                return new ResultModel<List<TredModel>>(result);
            } catch(Exception ex)
            {
                errMessage = $"Error in GetTredsList for {userName}: {ex.Message}";
                _logger.Warn(errMessage);
                return new ResultModel<List<TredModel>>(errMessage);
            }
        }
    }
}
