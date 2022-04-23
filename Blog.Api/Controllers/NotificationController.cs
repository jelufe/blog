using AutoMapper;
using Blog.Api.Responses;
using Blog.Domain.DAOs;
using Blog.Domain.DTOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Authorize]
    public class NotificationController : CustomControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(
            INotificationService notificationService,
            IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            IEnumerable<NotificationDao> notifications = new List<NotificationDao>();

            if (IsAdmin)
                notifications = await _notificationService.GetNotifications();
            else if (IsWriter)
                notifications = await _notificationService.GetNotifications(CurrentUserId);
            else if (IsReader)
                return Forbid();

            var response = new ApiResponse<IEnumerable<NotificationDao>>(notifications);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification([FromRoute] int id)
        {
            var notification = await _notificationService.GetNotification(id);
            var response = new ApiResponse<NotificationDao>(notification);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertNotification([FromBody] NotificationDto notificationDto)
        {
            if (!IsAdmin && !IsWriter)
                return Forbid();

            var notification = _mapper.Map<Notification>(notificationDto);
            await _notificationService.InsertNotification(notification);
            var response = new ApiResponse<Notification>(notification);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateNotification([FromBody] Notification notification)
        {
            if (!IsAdmin && !IsWriter)
                return Forbid();

            var result = await _notificationService.UpdateNotification(notification, IsAdmin, CurrentUserId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification([FromRoute] int id)
        {
            if (!IsAdmin && !IsWriter)
                return Forbid();

            var result = await _notificationService.DeleteNotification(id, IsAdmin, CurrentUserId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
