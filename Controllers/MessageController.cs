using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerServer.Models;
using MessengerServer.Repositories;

namespace MessengerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageRepository _messengerRepository;

        public MessageController(MessageRepository messengerRepository)
        {
            _messengerRepository = messengerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            var messages = await _messengerRepository.GetMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageById(Guid id)
        {
            var message = await _messengerRepository.GetMessageByIdAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByUserId(Guid userId)
        {
            var messages = await _messengerRepository.GetMessagesByUserIdAsync(userId);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage(Message message)
        {
            var createdMessage = await _messengerRepository.CreateMessageAsync(message);
            return CreatedAtAction(nameof(GetMessageById), new { id = createdMessage.Id }, createdMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(Guid id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            await _messengerRepository.UpdateMessageAsync(message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            var messageExists = _messengerRepository.MessageExists(id);
            if (!messageExists)
            {
                return NotFound();
            }

            await _messengerRepository.DeleteMessageAsync(id);

            return NoContent();
        }
    }
}