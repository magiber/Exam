import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Empleado } from '../../interfaces/empleado';
import { EmpleadoService } from '../../services/empleado.service';

@Component({
  selector: 'app-agregar-editar-empleado',
  templateUrl: './agregar-editar-empleado.component.html',
  styleUrl: './agregar-editar-empleado.component.css'
})
export class AgregarEditarEmpleadoComponent implements OnInit{
  loading: boolean = false;
  form: FormGroup;
  id: number;

  operacion: string = 'Agregar';

  constructor(private fb: FormBuilder,
    private _empleadoService: EmpleadoService,
    private _snackBar: MatSnackBar,
    private router: Router,
    private aRoute: ActivatedRoute) {
    this.form = this.fb.group({
      fotografia: ['', Validators.required],
      nombre: ['', Validators.required],
      apellidos: ['', Validators.required],
      puestoId: ['', Validators.required],
      fechaNacimineto: ['', Validators.required],
      direccion: ['', Validators.required],
      telefono: ['', Validators.required],
      correoElectronico: ['', Validators.required],
      estadoId: ['', Validators.required],
    })
    this.id = Number(this.aRoute.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {

    if(this.id != 0) {
      this.operacion = 'Editar';
      this.obtenerEmpleado(this.id)
    }
  }

    obtenerEmpleado(idEmpleado: number) {
      this.loading = true;
      this._empleadoService.getEmpleado(idEmpleado).subscribe(data => {
        this.form.patchValue({
          idEmpleado: data.idEmpleado,
          fotografia: data.fotografia,
          nombre: data.nombre,
          apellidos: data.apellidos,
          puestoId: data.puestoId,
          fechaNacimineto: data.fechaNacimineto,
          direccion: data.direccion,
          telefono: data.telefono,
          correoElectronico: data.correoElectronico,
          estadoId: data.estadoId
        })
        this.loading = false;
      })
    }

  agregarEditarEmpleado() {
    /* const nombre = this.form.get('nombre')?.value; */

    // Armamos el objeto
    const empleado: Empleado = {
      fotografia: this.form.value.fotografia,
      nombre: this.form.value.nombre,
      apellidos: this.form.value.apellidos,
      puestoId: this.form.value.puestoId,
      fechaNacimineto: this.form.value.fechaNacimineto,
      direccion: this.form.value.direccion,
      telefono: this.form.value.telefono,
      correoElectronico: this.form.value.correoElectronico,
      estadoId: this.form.value.estadoId
    }

    if(this.id != 0) {
      empleado.idEmpleado = this.id;
      this.editarEmpleado(this.id, empleado);
    } else {
      this.agregarEmpleado(empleado);
    }
  }

  editarEmpleado(id: number, empleado: Empleado) {
    this.loading = true;
    this._empleadoService.updateEmpleado(empleado).subscribe(() => {
      this.loading = false;
      this.mensajeExito('actualizado');
      this.router.navigate(['/listEmpleados']);
    })
  }

  agregarEmpleado(empleado: Empleado) {
      this._empleadoService.addEmpleado(empleado).subscribe(data => {
        this.mensajeExito('registrado');
        this.router.navigate(['/listEmpleados']);
      })
  }

  mensajeExito(texto: string) {
    this._snackBar.open(`El Empleado fue ${texto} con exito`,'', {
      duration: 4000,
      horizontalPosition: 'right',
    });
  }

}
