import { TarefaService } from './../../service/tarefa.service';
import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Tarefa } from 'src/app/model/tarefa';

@Component({
  selector: 'app-tarefa',
  templateUrl: './tarefa.component.html',
  providers: [MessageService, ConfirmationService]
})
export class TarefaComponent implements OnInit {

  exibirModal: boolean;

  tarefas: Tarefa[];

  tarefa: Tarefa;

  tarefasSelecionadas: Tarefa[];

  submitted: boolean;

  statuses: any[];


  constructor(private service: TarefaService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService) {

  }

  ngOnInit(): void {
    this.obterTarefas();
  }

  private obterTarefas() {
    this.service.obterTarefas().subscribe({
      next: (value) => {
        if (value.sucesso) {
          this.tarefas = value?.dados;
        } else {
          this.messageService.add({ severity: 'success', summary: 'Festa', detail: value.mensagens[0] });
        }
      },
      error: (e) => {
        this.messageService.add({ severity: 'error', key: "geral", summary: 'Falha de comunicação', detail: e });
      }
    });
  }



  incluir() {
    this.tarefa = {};
    this.submitted = false;
    this.exibirModal = true;
  }

  excluirTarefasSelecionadas() {
    this.confirmationService.confirm({
      message: 'Deseja excluir as tarefas selecionadas?',
      header: 'Confirmação',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        let codigos = this.tarefasSelecionadas.map(tarefa => tarefa.codigo);
        this.fnExcluir(codigos);
      }
    });
  }

  alterarTarefa(tarefa: Tarefa) {
    this.tarefa = { ...tarefa };
    this.exibirModal = true;
  }

  excluirTarefa(tarefa: Tarefa) {
    this.confirmationService.confirm({
      message: 'Deseja excluir a tarefa: ' + tarefa.codigo + '?',
      header: 'Confirmação',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.fnExcluir([tarefa.codigo]);
      }
    });
  }

  fnExcluir(codigos: number[]) {
    this.service.excluir(codigos).subscribe({
      next: (value) => {
        if (value.sucesso) {
          this.messageService.add({ severity: 'success', summary: 'Tarefa', detail: 'A tarefa foi excluida!', life: 3000 });
        } else {
          value.mensagens.forEach(mensagem => {
            this.messageService.add({ severity: 'error', summary: 'Tarefa', detail: mensagem, life: 3000 });
          });
        }
      },
      error: (e) => this.messageService.add({ severity: 'error', summary: 'Tarefa', detail: e, life: 3000 }),
      complete: () => this.obterTarefas()
    })
  }


  fecharModal() {
    this.exibirModal = false;
    this.submitted = false;
  }

  salvarTarefa() {
    this.submitted = true;

    this.messageService.clear("salvar");

    if (this.tarefa.nome.trim() && this.tarefa.descricao.trim()) {

      if (!this.tarefa.codigo) {

        this.service.incluir(this.tarefa).subscribe({
          next: (value) => {
            if (value.sucesso) {
              this.messageService.add({ severity: 'success', summary: 'Tarefa', detail: 'Tarefa inclusa', life: 3000 });
              this.obterTarefas();
              this.exibirModal = false;
              this.tarefa = {};
            } else {

              value.mensagens.forEach(mensagem => {
                this.messageService.add({ severity: 'error', key: "salvar", detail: mensagem });
              });

            }
          },
          error: (e) => this.messageService.add({ severity: 'error', key: "salvar", detail: e }),
        })

      }
      else {

        this.service.alterar(this.tarefa).subscribe({
          next: (value) => {
            if (value.sucesso) {
              this.messageService.add({ severity: 'success', summary: 'Tarefa', detail: 'Tarefa atualizada', life: 3000 });
              this.obterTarefas();
              this.exibirModal = false;
              this.tarefa = {};
            } else {

              value.mensagens.forEach(mensagem => {
                this.messageService.add({ severity: 'error', key: "salvar", detail: mensagem });
              });

            }
          },
          error: (e) => this.messageService.add({ severity: 'error', key: "salvar", detail: e }),
        })

      }


    }
  }
}
