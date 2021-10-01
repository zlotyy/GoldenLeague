import axios from "axios";

export default {
  GetUser() {
    return axios.post("login/authenticate", {
      login: "admin",
      password: "admin321"
    });
  }
};
