import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Empleado } from '../interfaces/empleado';

@Injectable({
  providedIn: 'root'
})
export class EmpleadoService {
  private myAppUrl: string = environment.endpoint;
  private myApiUrlL: string = 'api/Empleado/Listar';
  private myApiUrlB: string = 'api/Empleado/Buscar/';
  private myApiUrlD: string = 'api/Empleado/Eliminar/';
  private myApiUrlA: string = 'api/Empleado/Guardar';
  private myApiUrlU: string = 'api/Empleado/Editar';

  constructor(private http: HttpClient) { }

  getEmpleados(): Observable<Empleado[]> {
    return this.http.get<Empleado[]>(`${this.myAppUrl}${this.myApiUrlL}`);
  }

  getEmpleado(id: number): Observable<Empleado> {
    return this.http.get<Empleado>(`${this.myAppUrl}${this.myApiUrlB}${id}`);
  }

  deleteEmpleado(id: number): Observable<void> {
    return this.http.delete<void>(`${this.myAppUrl}${this.myApiUrlD}${id}`);
  }

  addEmpleado(empleado: Empleado): Observable<Empleado> {
    return this.http.post<Empleado>(`${this.myAppUrl}${this.myApiUrlA}`, empleado);
  }

  updateEmpleado(empleado: Empleado): Observable<void> {
    return this.http.put<void>(`${this.myAppUrl}${this.myApiUrlU}`, empleado);
  }
}
