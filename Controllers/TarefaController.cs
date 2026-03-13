using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefaBuscarId = _context.Tarefas.Find(id);

            if(tarefaBuscarId == null)
                return NotFound();
            return Ok(tarefaBuscarId);
            // TODO: Buscar o Id no banco utilizando o EF - FEITO
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound, - FEITO
            // caso contrário retornar OK com a tarefa encontrada - FEITO
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF - FEITO
            var tarefasTodas = _context.Tarefas.ToList();
            return Ok(tarefasTodas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefaBuscarTitulo = _context.Tarefas.Where(x => x.Titulo == titulo);

            if(tarefaBuscarTitulo == null)
                return NotFound();
            return Ok(tarefaBuscarTitulo);
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro - FEITO
            // Dica: Usar como exemplo o endpoint ObterPorData - FEITO
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro - FEITO
            // Dica: Usar como exemplo o endpoint ObterPorData - FEITO
            var tarefaBuscarStatusBd = _context.Tarefas.Where(x => x.Status == status).ToList();                           
            return Ok(tarefaBuscarStatusBd);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes) - FEITO
            _context.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro - FEITO
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes) - FEITO
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes) - FEITO
            return NoContent();
        }
    }
}
