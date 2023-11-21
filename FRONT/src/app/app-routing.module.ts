import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TarefaListarComponent } from "./pages/tarefa/tarefa-listar/tarefa-listar.component";
import { TarefaCadastrarComponent } from "./pages/tarefa/tarefa-cadastrar/tarefa-cadastrar.component";

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
    

];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
