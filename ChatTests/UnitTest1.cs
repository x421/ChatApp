using Chat.Controllers;
using Chat.Types;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;

namespace ChatTests
{
    public class Tests
    {
        [Test]
        public void MessageGetTest()
        {
            MessageController messageController = new MessageController();
            var result = messageController.Get(0);
            Assert.IsNotNull(result);
        }
        [Test]
        public void MessagePostTest()
        {
            MessageController messageController = new MessageController();
            Message msg = new Message();
            msg.login = "228";
            msg.message = "Love, love will tear us apart";
            var result = messageController.Post(msg);
            Assert.IsNotNull(result);
        }
        
    }
}