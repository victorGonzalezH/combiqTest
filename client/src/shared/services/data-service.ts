import http from '../http-common';

class DataService {
  
    getAll() {
    return http.get("/");
  }

  create(method: string, data: any) {
    return http.post(method, data);
  }

}

export default new DataService();
