import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Empleado } from '../../interfaces/empleado';
import { EmpleadoService } from '../../services/empleado.service';

@Component({
  selector: 'app-ver-empleado',
  templateUrl: './ver-empleado.component.html',
  styleUrl: './ver-empleado.component.css'
})
export class VerEmpleadoComponent implements OnInit, OnDestroy{
  id!: number;
  empleado!: Empleado;
  loading: boolean = false;

  routeSub!: Subscription;

  /*   empleado$!: Observable<Empleado> -- PIPE ASYNC */

  constructor(private _empleadoService: EmpleadoService,
    private aRoute: ActivatedRoute) {
    this.id = Number(this.aRoute.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {
    /*  this.empleado$ = this._empleadoService.getEmpleado(this.id)   --PIPE ASYNC */
  /*    this.routeSub =  this.aRoute.params.subscribe(data => {
      this.id = data['id'];
      this.obtenerEmpleado();
    }) */
    this.obtenerEmpleado();
  }

  ngOnDestroy(): void {
    /* this.routeSub.unsubscribe() */
  }

  obtenerEmpleado() {
    this.loading = true;
    this._empleadoService.getEmpleado(this.id).subscribe(data => {
      console.log(data);
      this.empleado = data;
      this.loading = false;
    })
  }

}
