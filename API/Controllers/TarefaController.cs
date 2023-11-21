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
	public IActionResult Alterar([FromRoute] int id)
	{
		try
		{
			Tarefa? tarefaCadastrada =
				_context.Tarefas.FirstOrDefault(x => x.TarefaId == id);

			if (tarefaCadastrada != null)
			{

				if (tarefaCadastrada.Status == "Não iniciada")
				{
					tarefaCadastrada.Status = "Em Andamento";
				} else if (tarefaCadastrada.Status == "Em Andamento") 
				{
					tarefaCadastrada.Status = "Concluída";
				} else if (tarefaCadastrada.Status == "Concluída") 
				{
					tarefaCadastrada.Status = "Não iniciada";
				}
			
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
	
	// GET: api/tarefa/listar/naoconcluidas
	[HttpGet("listar/naoconcluidas")]
	// public IActionResult Buscar()
	// {
	// 	try
	// 	{
	// 		Tarefa? tarefa = _context.Tarefas
	// 			.Include(x => x.Categoria)
	// 			.FirstOrDefault(x => x.Status == "Não iniciada" || x.Status == "Em Andamento");
	// 		if (tarefa == null)
	// 		{
	// 			return NotFound();
	// 		}
	// 		return Ok(tarefa);
	// 	}
	// 	catch (Exception e)
	// 	{
	// 		return BadRequest(e.Message);
	// 	}
		
	// } 
		public IActionResult ListarNaoConcluidas()
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
}
