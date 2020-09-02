using System;
using Application.Service;
using Application.UseCases.Obras.AddObras;
using Application.UseCases.Obras.EditObras;
using EldoradoService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EldoradoService.Controllers
{
    [Route("obraService")]
    [ApiController]
    public class ObraController : ControllerBase
    {
        private IObraService _obraService;

        public ObraController(IObraService obraService)
        {
            _obraService = obraService;
        }
        
        [HttpPost, Route("obra")]
        public IActionResult AddObra([FromBody] ObraRequestDTO request)
        {
            var result = _obraService.Handle( new AddObraRequestObject(request.DescricaoObra, 
                                                                                                request.DataInicioObras ,
                                                                                                request.DataEntregaEmpreendimento, 
                                                                                                request.DataValidadeGarantia,
                                                                                                request.ValorTotalAquisicao, 
                                                                                                request.PorcentagemParticipacao));

            return StatusCode(result.Result.StatusCode, result.Result);
        }
        
        [HttpPut, Route("obra/{idObra}")]
        public IActionResult EditObra([FromBody] ObraRequestDTO request, [FromRoute] string idObra)
        {
            var result = _obraService.Handle( new EditObraRequestObject(idObra,
                request.DescricaoObra, 
                request.DataInicioObras ,
                request.DataEntregaEmpreendimento, 
                request.DataValidadeGarantia,
                request.ValorTotalAquisicao, 
                request.PorcentagemParticipacao,
                request.StatusObra));

            return StatusCode(result.Result.StatusCode, result.Result);
        }
        
    }
}