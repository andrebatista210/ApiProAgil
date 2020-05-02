import { RedeSocial } from './RedeSocial';
import { Evento } from './Evento';

export interface Palestrante {
  id: number;
  nome: string;
  miniCurriculo: string;
  imageURL: string;
  telefone: string;
  email: string;
  redeSociais: RedeSocial[];
  palestranteEventos: Evento[];
}
