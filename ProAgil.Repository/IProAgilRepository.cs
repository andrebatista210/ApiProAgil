using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository {
    public interface IProAgilRepository {
        //GERAL
        void Add<T> (T entity) where T : class;
        void Delete<T> (T entity) where T : class;
        void Update<T> (T entity) where T : class;
        Task<bool> SaveChangesAsync ();

        //EVENTOS

        Task<Evento[]> GetAllEventoAsyncByName (string tema, bool inludePalestrantes);
        Task<Evento[]> GetAllEventoAsync ( bool inludePalestrantes);
        Task<Evento> GetEventoAsyncById (int EventoId, bool inludePalestrantes);

        // PALESTRANTE

        Task<Palestrante[]> GetPalestrantesAsyncByName (string PalestranteByName, bool includeEventos);
        Task<Palestrante> GetPalestrantesAsyncById (int PalestranteId, bool includeEventos);

    }
}