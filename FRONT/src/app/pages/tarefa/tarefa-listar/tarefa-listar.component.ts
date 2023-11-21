import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Tarefa } from '../../../models/tarefa.model';


@Component({
  selector: 'app-tarefa-listar',
  templateUrl: './tarefa-listar.component.html',
  styleUrl: './tarefa-listar.component.css'
})


export class TarefaListarComponent {


  tarefas : Tarefa[] = []


  constructor(
    private client: HttpClient,){}


  ngOnInit() : void{
  console.log("O componente foi carregado!");


  this.client.get<Tarefa[]>("https://localhost:7015/api/tarefa/listar ")
  .subscribe({
  //Requisição com sucesso
    next: (tarefas) => {
      this.tarefas = tarefas;
      console.table(tarefas);
    },
    //Requisição com erro
    error: (erro) => {
      console.log(erro);
    }
    })
    }
  
}

