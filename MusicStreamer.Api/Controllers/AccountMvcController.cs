using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Extensions;
using MusicStreamer.Api.Models;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[Route("app/account")]
public sealed class AccountMvcController(IServicoAutenticacao authService) : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetCurrentUser() is not null)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View(new LoginViewModel());
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var response = await authService.LoginAsync(new EntrarDto(model.Email, model.Password), cancellationToken);
        if (response is null)
        {
            model.ErrorMessage = "Credenciais invalidas.";
            return View(model);
        }

        HttpContext.Session.SetCurrentUser(new SessionUserViewModel
        {
            UserId = response.UserId,
            DisplayName = response.DisplayName,
            Email = response.Email,
            Token = response.Token
        });

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost("register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await authService.RegisterAsync(new CadastrarUsuarioDto(model.DisplayName, model.Email, model.Password), cancellationToken);
            HttpContext.Session.SetCurrentUser(new SessionUserViewModel
            {
                UserId = response.UserId,
                DisplayName = response.DisplayName,
                Email = response.Email,
                Token = response.Token
            });

            return RedirectToAction("Index", "Dashboard");
        }
        catch (InvalidOperationException ex)
        {
            model.ErrorMessage = ex.Message;
            return View(model);
        }
    }

    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.ClearCurrentUser();
        return RedirectToAction(nameof(Login));
    }
}

