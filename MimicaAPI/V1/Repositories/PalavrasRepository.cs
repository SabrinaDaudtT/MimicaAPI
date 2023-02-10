using Microsoft.EntityFrameworkCore;
using MimicaAPI.Database;
using MimicaAPI.Helpers;
using MimicaAPI.V1.Models;
using MinicAPI.V1.Repositories.Contracts;
using System;
using System.Linq;

namespace MinicAPI.V1.Repositories
{
    public class PalavrasRepository : IPalavrasRepository
    {
        private readonly MimicaContext _banco;
        //Conexão com banco de dados
        public PalavrasRepository(MimicaContext banco) 
        {
            _banco = banco;
        }

        public PaginationList<Palavra> ObterTodos(PalavrasUrlQuery query)
        {
            var list = new PaginationList<Palavra>();
            var item = _banco.Palavras.AsNoTracking().AsQueryable();
            if (query.data.HasValue)
            {
                item = item.Where(a => a.Criado > query.data.Value || a.Atualizado > query.data.Value);
            }
            if (query.pagNumero.HasValue)
            {
                var quantidadeTotal = item.Count();
                item = item.Skip((query.pagNumero.Value - 1) * query.NumRegistroPag.Value).Take(query.NumRegistroPag.Value);

                var paginacao = new Paginacao();
                paginacao.NumeroPagina = query.pagNumero.Value;
                paginacao.RegistroPorPagina = query.NumRegistroPag.Value;
                paginacao.TotalRegistros = quantidadeTotal;
                paginacao.TotalPaginas = (int)Math.Ceiling((double)quantidadeTotal / query.NumRegistroPag.Value);

                list.Paginacao = paginacao;
            }

            list.Results.AddRange(item.ToList());

            return list;
        }

        public Palavra Obter(int id)
        {
            var objPalavra = _banco.Palavras.AsNoTracking().FirstOrDefault(a => a.Id == id);

            return (objPalavra);
        }

        public void Cadastrar(Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();
        }

        public void Atualizar(Palavra palavra)
        {
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
        }

        public void Delete(int id)
        {
            var palavra = Obter(id);

            palavra.Ativo = false;
            _banco.Palavras.Update(palavra);

            _banco.SaveChanges();
        }
    }
}
