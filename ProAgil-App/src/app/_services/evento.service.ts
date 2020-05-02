import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseURL = 'http://localhost:5000/api/evento';
  constructor(private http: HttpClient) {

  }

  getAllEvento(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL);
  }

  getEventoByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema} `);
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id} `);
  }

  postEvento(evento: Evento) { //  METODO PARA ENVIAR OS DADOS PARA O BANCO
    return this.http.post(this.baseURL, evento); // URL DA API, E O EVENTO QUE TRAREMOS DO FORMULARIO
  }

  putEvento(evento: Evento) { //  METODO PARA EDITAR OS DADOS PARA O BANCO
    return this.http.put(`${this.baseURL}/${evento.id} `, evento); // URL DA API, E O EVENTO QUE TRAREMOS DO FORMULARIO
  }

  deleteEvento(id: number) { //  METODO PARA EDITAR OS DADOS PARA O BANCO
    return this.http.delete(`${this.baseURL}/${id}`); // URL DA API, E O EVENTO QUE TRAREMOS DO FORMULARIO

  }
}
