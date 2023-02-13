using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MimicaAPI.Helpers;
using MimicaAPI.V1.Models;
using MimicaAPI.V1.Models.DTO;
using MimicaAPI.V1.Repositories.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MimicaAPI.V1.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1")]
    public class PalavrasController : ControllerBase
    {
        private readonly IPalavrasRepository _repository;
        private readonly IMapper _mapper;
        //Conexão com banco de dados
        public PalavrasController(IPalavrasRepository repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }

        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        [HttpGet("",Name = "ObterTodos")]
        public ActionResult ObterTodos([FromQuery] PalavrasUrlQuery query)
        {
            var item = _repository.ObterTodos(query);

            if (item.Results.Count == 0)
                return NotFound();

            var list = _mapper.Map<PaginationList<Palavra>, PaginationList<PalavraDTO>>(item);

            CriarLinksListPalavrasDTO(query, item, list);

            return Ok(list);
        }

        private void CriarLinksListPalavrasDTO(PalavrasUrlQuery query, PaginationList<Palavra> item, PaginationList<PalavraDTO> list)
        {
            foreach (var palavra in list.Results)
            {
                palavra.Links = new List<LinkDTO>();
                palavra.Links.Add(
                    new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavra.Id }), "GET")
                    );
            }

            list.Links.Add(new LinkDTO("self", Url.Link( "ObterTodos", query), "GET"));

            if (item.Paginacao != null)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(item.Paginacao));

                if (query.pagNumero + 1 <= item.Paginacao.TotalPaginas)
                {
                    var queryString = new PalavrasUrlQuery() { pagNumero = query.pagNumero + 1, NumRegistroPag = query.NumRegistroPag, data = query.data };
                    list.Links.Add(new LinkDTO("netx", Url.Link("ObterTodos", queryString), "GET"));

                }
                if (query.pagNumero - 1 > 0)
                {
                    var queryString = new PalavrasUrlQuery() { pagNumero = query.pagNumero - 1, NumRegistroPag = query.NumRegistroPag, data = query.data };
                    list.Links.Add(new LinkDTO("prev", Url.Link("ObterTodos", queryString), "GET"));
                }
            }
        }

        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        [HttpGet("{id}", Name = "ObterPalavra")]
        public ActionResult Obter(int id)
        {
            var objPalavra = _repository.Obter(id);

            if (objPalavra == null)
                return NotFound();

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(objPalavra);
            palavraDTO.Links.Add(
                new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavraDTO.Id }), "GET")
                );
            palavraDTO.Links.Add(
                new LinkDTO("update", Url.Link("AtualizarPalavra", new { id = palavraDTO.Id }), "PUT")
                );
            palavraDTO.Links.Add(
                new LinkDTO("delete", Url.Link("ExcluirPalavra", new { id = palavraDTO.Id }), "DELTE")
               );

            return Ok(palavraDTO);
        }

        //  api/palavras/(id, nome, ativo ...)
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            if (palavra == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            palavra.Ativo = true;
            palavra.Criado = DateTime.Now;
            _repository.Cadastrar(palavra);

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(palavra);

            palavraDTO.Links.Add(
             new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavraDTO.Id }), "GET")
             );

            return Created($"/api/palavras/{palavra.Id}", palavraDTO);
        }

        //  api/palavras/{id}(id, nome, ativo ...)
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        [HttpPut("{id}", Name = "AtualizarPalavra")]
        public ActionResult Atualizar(int id, [FromBody] Palavra palavra)
        {
            var obj = _repository.Obter(id);

            if (obj == null)
                return NotFound();

            if (palavra == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            palavra.Id = id;
            palavra.Ativo = obj.Ativo;
            palavra.Criado = obj.Criado;
            palavra.Atualizado = DateTime.Now;
            _repository.Atualizar(palavra);

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(palavra);
            palavraDTO.Links.Add(
             new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavraDTO.Id }), "GET")
             );

            return Ok();
        }

        //  api/palavras/{id}
        [MapToApiVersion("1.1")]
        [HttpDelete("{id}", Name = "ExcluirPalavra")]
        public ActionResult Delete(int id)
        {
            var palavra = _repository.Obter(id);
            if (palavra == null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
    }
}
