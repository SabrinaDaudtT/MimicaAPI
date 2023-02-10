using MimicaAPI.Helpers;
using MimicaAPI.V1.Models;

namespace MinicAPI.V1.Repositories.Contracts
{
    public interface IPalavrasRepository
    {
        PaginationList<Palavra> ObterTodos(PalavrasUrlQuery query);

        Palavra Obter(int id);

        void Cadastrar(Palavra palavra);

        void Atualizar(Palavra palavra);

        void Delete(int id);
    }
}
