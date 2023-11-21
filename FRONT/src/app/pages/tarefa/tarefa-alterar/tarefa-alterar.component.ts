import { Component } from '@angular/core';
import { Tarefa } from '../../../models/tarefa.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Categoria } from '../../../models/categoria.model';

@Component({
  selector: 'app-tarefa-alterar',
  templateUrl: './tarefa-alterar.component.html',
  styleUrls: ['./tarefa-alterar.component.css'], 
})
export class TarefaAlterarComponent {
  titulo: string = "";
  descricao: string = "";
  status: string = "Não iniciada";
  categoriaId: number = 0;
  categorias: Categoria[] = [];

  constructor(
    private client: HttpClient,
    private router: Router,
  ) {}

    ngOnInit(): void {
      this.client
        .get<Categoria[]>("https://localhost:7015/api/categoria/listar")
        .subscribe({
          //A requição funcionou
          next: (categorias) => {
            console.table(categorias);
            this.categorias = categorias;
          },
          //A requição não funcionou
          error: (erro) => {
            console.log(erro);
          },
        });
    }

    alterar(): void {
    let tarefa: Tarefa = {
      titulo: this.titulo,
      descricao: this.descricao,
      status: this.status,
      categoriaId: this.categoriaId,
    };

    this.client
      .patch<Tarefa>(
        "https://localhost:7015/api/tarefa/alterar/{id}",
        tarefa
      )
      .subscribe({
        //A requição funcionou
        next: (tarefa) => {
          console.log("Tarefa cadastrada com sucesso:", tarefa);
          this.router.navigate(["pages/tarefa/listar"]);
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }
}
