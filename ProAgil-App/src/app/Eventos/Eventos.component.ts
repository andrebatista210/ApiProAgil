import { Evento } from './../_models/Evento';
import { EventoService } from './../_services/evento.service';
import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, ptBrLocale, BsLocaleService } from 'ngx-bootstrap';

defineLocale('pt-br', ptBrLocale);

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-Eventos',
  templateUrl: './Eventos.component.html',
  styleUrls: ['./Eventos.component.css']
})
export class EventosComponent implements OnInit {
  eventoFiltrados: Evento[];
  eventos: Evento[];

  evento: Evento;
  modosalvar = 'post';

  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  bodyDeletarEvento = '';

  // tslint:disable-next-line:variable-name
  _filtroLista: string;

  constructor(
    private eventoService: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService
  ) {
    this.localeService.use('pt-br');
  }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(values: string) {
    this._filtroLista = values;
    this.eventoFiltrados = this.filtroLista
      ? this.filtraEventos(this.filtroLista)
      : this.eventos;
  }

  editarEvento(evento: Evento, template: any) {
    this.modosalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  novoEvento(template: any) {
    this.modosalvar = 'post';
    this.openModal(template);
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o evento: ${evento.tema} com Código ${evento.id} `;
  }

  confirmeDelete(template: any) {
    console.log('Aquio ID ' + this.evento.id);
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
      }, error => {
        console.log(error);
      }
    );

  }


  openModal(template: any) { // ABRE O MODAL DO FORMULARIO
    this.registerForm.reset();
    template.show();
  }



  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dateEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(5000)]],
      imageURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  salvarAlteracao(template) {
    if (this.registerForm.valid) { // VERIFICA SE OS DADOS ESTAÃO PREENCHIDOS NO FORMULARIO
      if (this.modosalvar === 'post') { // VERIFICA SE UMA INSEÇÃO OU UM EDITAR
        this.evento = Object.assign({}, this.registerForm.value); // COPIA OS DADOS DO FORMULARIO PARA A VARIAVEL
        this.eventoService.postEvento(this.evento) // ISSO FUNCIONA MAS É UM OBSERVABLE ENTÃO PRECISA DO SUBSCRIBE
          .subscribe( // PRIMEIRO PARAMENTRO SUCESSO - SEGUNDO ERRO
            () => {
              template.hide();
              this.getEventos();
            }, error => {
              console.log(error);
            }
          );
      } else {
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value); // COPIA OS DADOS DO FORMULARIO PARA A VARIAVEL
        this.eventoService.putEvento(this.evento) // ISSO FUNCIONA MAS É UM OBSERVABLE ENTÃO PRECISA DO SUBSCRIBE
          .subscribe( // PRIMEIRO PARAMENTRO SUCESSO - SEGUNDO ERRO
            () => {
              template.hide();
              this.getEventos();
            }, error => {
              console.log(error);
            }
          );
      }


    }
  }

  alterarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  filtraEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => (evento.tema.toLocaleLowerCase() + evento.local.toLocaleLowerCase()).indexOf(filtrarPor) !== -1
    );
  }

  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      // tslint:disable-next-line: variable-name
      (_eventos: Evento[]) => {
        this.eventos = _eventos.sort((x, y) => y.id - x.id);
        this.eventoFiltrados = this.eventos;
        console.log(_eventos);
      },
      error => {
        console.log(error);
      }
    );
  }
}
