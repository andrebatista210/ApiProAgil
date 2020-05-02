using System;
using System.Collections.Generic;

namespace ProAgil.Api.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime DateEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string ImageURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<LoteDto> Lote { get; set; }
        public List<RedeSocialDto> RedeSociais { get; set;  }
        public List<PalestranteDto> Palestrantes { get; set; }
    }
}