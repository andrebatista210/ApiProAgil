using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository {
    public class ProAgilRepository : IProAgilRepository {
        public readonly ProAgilContext _context;

        public ProAgilRepository (ProAgilContext context) {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //GERAL
        public void Add<T> (T entity) where T : class {
            _context.Add (entity);
        }

        public void Delete<T> (T entity) where T : class {
            var teste = entity;
            _context.Remove (entity);
        }

        public async Task<bool> SaveChangesAsync () {
            return (await _context.SaveChangesAsync ()) > 0;
        }

        public void Update<T> (T entity) where T : class {
            _context.Update (entity);
        }

        //GET
        public async Task<Evento[]> GetAllEventoAsync (bool inludePalestrantes = false) {

            IQueryable<Evento> query = _context.Eventos.Include (c => c.Lote).Include (c => c.RedeSociais);

            if (inludePalestrantes) {
                query = query.Include (pe => pe.PalestranteEventos).ThenInclude (p => p.Palestrante);
            }

            query = query.AsNoTracking()
                        .OrderBy (c => c.Id);

            return await query.ToArrayAsync ();
        }

        public async Task<Evento[]> GetAllEventoAsyncByName (string tema, bool inludePalestrantes) {

            IQueryable<Evento> query = _context.Eventos.Include (c => c.Lote).Include (c => c.RedeSociais);

            if (inludePalestrantes) {
                query = query.Include (pe => pe.PalestranteEventos).ThenInclude (p => p.Palestrante);
            }

            query = query.AsNoTracking().OrderBy (c => c.Id).Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync ();
        
        }

        public async Task<Evento> GetEventoAsyncById (int EventoId, bool inludePalestrantes) {
           IQueryable<Evento> query = _context.Eventos.Include (c => c.Lote).Include (c => c.RedeSociais);

            if (inludePalestrantes) {
                query = query.Include (pe => pe.PalestranteEventos).ThenInclude (p => p.Palestrante);
            }
            
            query = query.AsNoTracking().OrderBy (c => c.Id).Where(c => c.Id == EventoId);

            return await query.FirstOrDefaultAsync ();
        }


        //PALESTRANTE

        public async Task<Palestrante> GetPalestrantesAsyncById (int PalestranteId, bool includeEventos) {

             IQueryable<Palestrante> query = _context.Palestrantes
                                                .Include (c => c.RedeSociais);

            if (includeEventos) {
                query = query.Include (pe => pe.PalestranteEventos).ThenInclude (e => e.Evento);
            }
            
            query = query.AsNoTracking().Where(c => c.Id == PalestranteId);

            return await query.FirstOrDefaultAsync ();
        }

        public async Task<Palestrante[]> GetPalestrantesAsyncByName (string PalestranteByName,bool includeEventos = false) {

             IQueryable<Palestrante> query = _context.Palestrantes.Include (c => c.RedeSociais);

            if (includeEventos) {
                query = query.Include (pe => pe.PalestranteEventos).ThenInclude (e => e.Evento);
            }

            query = query.AsNoTracking().Where(n => n.Nome.ToLower().Contains(PalestranteByName.ToLower()));

            return await query.ToArrayAsync ();
        }

        
    }
}