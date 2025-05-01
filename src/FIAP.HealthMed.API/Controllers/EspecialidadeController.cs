using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Model;
using FIAP_HealthMed.Application.Model.Especialidade;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_HealthMed.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeApplicationService _especialidadeService;

        public EspecialidadeController(IEspecialidadeApplicationService especialidadeService)
        {
            _especialidadeService = especialidadeService;
        }

        /// <summary>
        /// Obter todas as especialidades
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var result = await _especialidadeService.ObterTodas();
            return Ok(result);
        }

        /// <summary>
        /// Obter uma especialidade por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var result = await _especialidadeService.ObterPorId(id);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Cadastrar uma nova especialidade
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CadastrarEspecialidadePost([FromBody] EspecialidadeModelRequest request)
        {
            var result = await _especialidadeService.CadastrarEspecialidade(request);
            return Ok(result);
        }
    }
}
