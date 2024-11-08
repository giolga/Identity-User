﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IdentityApp.Data;
using IdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using IdentityApp.Authorization;

namespace IdentityApp.Views.Invoices
{
    public class DetailsModel : DI_BasePageModel
    {

        public DetailsModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
        }

        public Invoice Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Invoice = await Context.Invoice.FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (Invoice == null)
            {
                return NotFound();
            }

            var isCreator = await AuthorizationService.AuthorizeAsync(User, Invoice, InvoiceOperations.Read);

            var isManager = User.IsInRole(Constants.InvoiceManagersRole);

            if (isCreator.Succeeded == false && isManager == false)
            {
                return Forbid();
            }

            return Page();
        }
    }
}
