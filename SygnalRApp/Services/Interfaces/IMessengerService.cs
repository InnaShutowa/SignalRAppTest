﻿using System;
using System.Collections.Generic;

using SignalRApp.Models;
using SignalRApp.Models.MessagerModels;

namespace SignalRApp.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с сообщениями
    /// </summary>
    public interface IMessengerService
    {
        /// <summary>
        /// Добавляет сообщение в базу
        /// </summary>
        /// <param name="authorUserName">Имя пользователя автора</param>
        /// <param name="recipientUserName">Имя пользователя получателя</param>
        /// <param name="text">Текст сообщения</param>
        /// <returns>Результат - успешно или текст ошибки</returns>
        ResultModel AddMessage(string authorUserName,
            string recipientUserName,
            string text);

        /// <summary>
        /// Получаем список сообщений между конкретными пользователями, отсортированный от более поздних сообщений к более ранним
        /// </summary>
        /// <param name="currentUserId">Id текущего пользователя</param>
        /// <param name="recipientUserId">Id получателя</param>
        /// <returns>Список сообщений</returns>
        ResultDataModel<List<MessageModel>> GetMessagesList(Guid? currentUserId, Guid? recipientUserId);

        /// <summary>
        /// Получаем список тредов
        /// </summary>
        /// <param name="userId">Id пользователя в системе</param>
        /// <returns>Список чатов</returns>
        ResultDataModel<List<TredModel>> GetTredsList(Guid? userId);
    }
}
