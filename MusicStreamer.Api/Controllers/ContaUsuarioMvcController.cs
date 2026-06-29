using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Extensions;
using MusicStreamer.Api.Models;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[Route("app/account")]
public sealed class ContaUsuarioMvcController(IServicoAutenticacao servicoAutenticacao) : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetCurrentUser() is not null)
        {
            return RedirectToAction("Home", "Painel");
        }

        return View(new EntrarViewModel());
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(EntrarViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var response = await servicoAutenticacao.LoginAsync(new EntrarDto(model.Email, model.Password), cancellationToken);
        if (response is null)
        {
            model.ErrorMessage = "Credenciais invalidas.";
            return View(model);
        }

        HttpContext.Session.SetCurrentUser(new UsuarioSessaoViewModel
        {
            UserId = response.UserId,
            DisplayName = response.DisplayName,
            Email = response.Email,
            Token = response.Token,
            SubscriptionPlanId = response.SubscriptionPlanId
        });

        return RedirectToAction("Home", "Painel");
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(new CadastroViewModel());
    }

    [HttpPost("register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(CadastroViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await servicoAutenticacao.RegisterAsync(new CadastrarUsuarioDto(model.DisplayName, model.Email, model.Password), cancellationToken);
            HttpContext.Session.SetCurrentUser(new UsuarioSessaoViewModel
            {
                UserId = response.UserId,
                DisplayName = response.DisplayName,
                Email = response.Email,
                Token = response.Token,
                SubscriptionPlanId = response.SubscriptionPlanId
            });

            return RedirectToAction("Home", "Painel");
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

