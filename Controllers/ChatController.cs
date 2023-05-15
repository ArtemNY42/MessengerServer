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
    public class ChatController : ControllerBase
    {
        private readonly ChatRepository _chatRepository;

        public ChatController(ChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
        {
            var chats = await _chatRepository.GetChatsAsync();
            return Ok(chats);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChatById(int id)
        {
            var chat = await _chatRepository.GetChatByIdAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            return Ok(chat);
        }

        [HttpPost]
        public async Task<ActionResult<Chat>> CreateChat(Chat chat)
        {
            var createdChat = await _chatRepository.CreateChatAsync(chat);
            return CreatedAtAction(nameof(GetChatById), new { id = createdChat.ChatId }, createdChat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChat(int id, Chat chat)
        {
            if (id != chat.ChatId)
            {
                return BadRequest();
            }

            await _chatRepository.UpdateChatAsync(chat);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var chatExists = _chatRepository.ChatExists(id);
            if (!chatExists)
            {
                return NotFound();
            }

            await _chatRepository.DeleteChatAsync(id);

            return NoContent();
        }
    }
}