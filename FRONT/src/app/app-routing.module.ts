import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TarefaListarComponent } from "./pages/tarefa/tarefa-listar/tarefa-listar.component";
import { TarefaCadastrarComponent } from "./pages/tarefa/tarefa-cadastrar/tarefa-cadastrar.component";
import { TarefaAlterarComponent } from "./pages/tarefa/tarefa-alterar/tarefa-alterar.component";


const routes: Routes = [
    {
        path: "",
        component: TarefaListarComponent,
      },
      {
        path:"pages/tarefa/listar",
        component: TarefaListarComponent,
      },
      {
        path:"pages/tarefa/cadastrar",
        component: TarefaCadastrarComponent,
      },
      {
        path:"pages/tarefa/alterar",
        component: TarefaAlterarComponent,
      },
    

];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
exports: [RouterModule],
})
export class AppRoutingModule {}
