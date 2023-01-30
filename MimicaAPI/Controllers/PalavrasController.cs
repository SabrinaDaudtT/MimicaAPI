using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MimicaAPI.Helpers;
using MimicaAPI.Models;
using MinicAPI.Models.DTO;
using MinicAPI.Repositories.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MimicaAPI.Controllers
{
    [Route("api/palavras")]
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

        [Route("")]
        [HttpGet]
        public ActionResult ObterTodos([FromQuery] PalavrasUrlQuery query)
        {
            var item = _repository.ObterTodos(query);

            if (item.Count == 0)
                return NotFound();

            if (item.Paginacao != null)
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(item.Paginacao));

            var list = _mapper.Map<PaginationList<Palavra>, PaginationList<PalavraDTO>>(item);

            foreach (var palavra in list)
            {
                palavra.Links = new List<LinkDTO>();
                palavra.Links.Add(
                    new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavra.Id }), "GET")
                    );
                palavra.Links.Add(
              new LinkDTO("update", Url.Link("AtualizarPalavra", new { id = palavra.Id }), "PUT")
              );
                palavra.Links.Add(
              new LinkDTO("delete", Url.Link("ExcluirPalavra", new { id = palavra.Id }), "DELTE")
              );
            }
            return Ok(list);
        }

        [HttpGet("{id}", Name = "ObterPalavra")]
        public ActionResult Obter(int id)
        {
            var objPalavra = _repository.Obter(id);

            if (objPalavra == null)
                return NotFound();

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(objPalavra);
            palavraDTO.Links = new List<LinkDTO>();
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
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            _repository.Cadastrar(palavra);

            return Created($"/api/palavras/{palavra.Id}", palavra);
        }

        //  api/palavras/{id}(id, nome, ativo ...)
        [HttpPut("{id}", Name = "AtualizarPalavra")]
        public ActionResult Atualizar(int id, [FromBody] Palavra palavra)
        {
            var obj = _repository.Obter(id);

            if (obj == null)
                return NotFound();

            palavra.Id = id;
            _repository.Atualizar(palavra);

            return Ok();
        }

        //  api/palavras/{id}
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
