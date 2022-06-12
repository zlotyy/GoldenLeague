import axios from "@/plugins/axios.js";

export default {
  ApiTest() {
    return axios.get("/api-test");
  },
};
