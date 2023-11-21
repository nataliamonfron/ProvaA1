using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers;

[Route("api/tarefa")]
[ApiController]
public class TarefaController : ControllerBase
{
	private readonly AppDataContext _context;

	public TarefaController(AppDataContext context) =>
		_context = context;

	// GET: api/tarefa/listar
	[HttpGet]
	[Route("listar")]
	public IActionResult Listar()
	{
		try
		{
			List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria).ToList();
			return Ok(tarefas);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}

	// POST: api/tarefa/cadastrar
	[HttpPost]
	[Route("cadastrar")]
	public IActionResult Cadastrar([FromBody] Tarefa tarefa)
	{
		try
		{
			Categoria? categoria = _context.Categorias.Find(tarefa.CategoriaId);
			if (categoria == null)
			{
				return NotFound();
			}
			tarefa.Categoria = categoria;
			_context.Tarefas.Add(tarefa);
			_context.SaveChanges();
			return Created("", tarefa);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	[HttpPatch]
	[Route("alterar/{id}")]
	public IActionResult Alterar([FromRoute] int id, [FromBody] Tarefa tarefa)
	{
		try
		{
			Tarefa? tarefaCadastrada =
				_context.Tarefas.FirstOrDefault(x => x.TarefaId == id);

			if (tarefaCadastrada != null)
			{

				Categoria? categoria =
					_context.Categorias.Find(tarefa.CategoriaId);
				if (categoria == null)
				{
					return NotFound();
				}
				if (tarefaCadastrada.Status == "Não iniciada")
				{
					tarefaCadastrada.Status = "Andamento";
				} else if (tarefaCadastrada.Status == "Andamento") 
				{
					tarefaCadastrada.Status = "Concluída";
				} else if (tarefaCadastrada.Status == "Concluída") 
				{
					tarefaCadastrada.Status = "Não iniciada";
				}
				tarefaCadastrada.Categoria = tarefaCadastrada.Categoria;
				tarefaCadastrada.Titulo = tarefaCadastrada.Titulo;
				tarefaCadastrada.Descricao = tarefaCadastrada.Descricao;				
				_context.Tarefas.Update(tarefaCadastrada);
				_context.SaveChanges();
				return Ok();
			}
			return NotFound();
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	// // GET: api/tarefa/listar/{naoiniciada}
	// [HttpGet("listar/")]
	// public IActionResult Buscar([FromRoute] string cpf, int mes, int ano)
	// {
	// 	try
	// 	{

	// 		Tarefa? tarefaCadastrada =_context.Tarefas
	// 		.Include(x => x.Categoria)
	// 		.FirstOrDefault(x => x.Mes == mes || x.Ano == ano);
			
	// 		if (tarefaCadastrada != null)
	// 		{
	// 			return Ok(tarefaCadastrada);
	// 		}
	// 		else 
	// 		{
	// 			return NotFound();
	// 		}
	// 	}
	// 	catch (Exception e)
	// 	{
	// 		return BadRequest(e.Message);
	// 	}
	// }
}
