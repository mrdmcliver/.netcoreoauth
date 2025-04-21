﻿using Microsoft.AspNetCore.Mvc;

namespace OAuthTest.Exceptions
{
    public abstract class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
        }

        public abstract IActionResult GetResponse();
    }
}
