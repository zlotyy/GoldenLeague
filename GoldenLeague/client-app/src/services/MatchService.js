import axios from "@/plugins/axios.js";
const API_URL = "/matches";

export default {
  GetCurrentGameweekNo() {
    return axios.get(`${API_URL}/current-gameweek-no`);
  },
  GetCurrentGameweekMatches() {
    return axios.get(`${API_URL}/current-gameweek`);
  },
};
