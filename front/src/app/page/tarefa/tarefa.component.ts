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
      message: 'Are you sure you want to delete the selected products?',
      header: 'Confirmação',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {


        // this.tarefa = this.products.filter(val => !this.selectedProducts.includes(val));
        // this.selectedProducts = null;
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Products Deleted', life: 3000 });

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
        // this.tarefas = this.tarefas.filter(x => x.codigo !== tarefa.codigo);
        // this.tarefa = {};
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'A tarefa foi excluida!', life: 3000 });
      }
    });
  }

  fecharModal() {
    this.exibirModal = false;
    this.submitted = false;
  }

  salvar() {
    this.submitted = true;

    // if (this.product.name.trim()) {
    //   if (this.product.id) {
    //     this.products[this.findIndexById(this.product.id)] = this.product;
    //     this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Product Updated', life: 3000 });
    //   }
    //   else {
    //     this.product.id = this.createId();
    //     this.product.image = 'product-placeholder.svg';
    //     this.products.push(this.product);
    //     this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Product Created', life: 3000 });
    //   }

    //   this.products = [...this.products];
    //   this.productDialog = false;
    //   this.product = {};
    // }
  }

  findIndexById(id: string): number {
    let index = -1;
    // for (let i = 0; i < this.products.length; i++) {
    //   if (this.products[i].id === id) {
    //     index = i;
    //     break;
    //   }
    // }

    return index;
  }
}
