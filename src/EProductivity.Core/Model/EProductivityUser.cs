﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EProductivity.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EProductivity.Core.Model
{
    public class EProductivityUser : IdentityUser
    {
        public Organization Organization { get; set; }
        public Guid OrganizationId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<EProductivityUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}