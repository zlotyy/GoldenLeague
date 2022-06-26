import axios from "@/plugins/axios.js";
const API_LEAGUES_URL = "bookmakerLeagues"; // nie zmieniać na kebab-case, żeby uniknąć błędu 405

export default {
  async LeagueCreate(model) {
    return axios.post(API_LEAGUES_URL, model);
  },
};
