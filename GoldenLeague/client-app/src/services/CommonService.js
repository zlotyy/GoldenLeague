import axios from "@/plugins/axios.js";
const API_URL = "/common";

export default {
  ApiTest() {
    return axios.get("/api-test");
  },
  async GetCompetitions() {
    return axios.get(`${API_URL}/competitions`);
  },
};
