import { Lote } from './Lote';
import { Palestrante} from './Palestrante';
import { RedeSocial } from './RedeSocial';

export interface Evento {
      id: number;
      local: string;
      dateEvento: Date;
      tema: string;
      qtdPessoas: number;
      imageURL: string;
      telefone: string;
      email: string;
      lote: Lote[];
      redeSociais: RedeSocial[];
      palestranteEventos: Palestrante[];
}
