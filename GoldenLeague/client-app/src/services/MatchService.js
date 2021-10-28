import axios from "@/plugins/axios.js";
const API_URL = "matches";

const API_URL_TMP = "users";
const userId = "35CE5DF3-CBCD-4A43-9582-A51CBEC26B91";

export default {
  GetCurrentGameweek() {
    return axios.get(`${API_URL}/current-gameweek`);
  },
  GetCurrentGameweekMatches() {
    // return axios.get(`${API_URL}/current-gameweek-matches`);
    return axios.get(`${API_URL_TMP}/${userId}/match-betting`);
  },
};
