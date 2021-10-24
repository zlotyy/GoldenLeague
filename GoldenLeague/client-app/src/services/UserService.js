import axios from "@/plugins/axios.js";
const API_URL = "users";

const userId = "35CE5DF3-CBCD-4A43-9582-A51CBEC26B91";

export default {
  GetMatchBetting() {
    return axios.get(`${API_URL}/${userId}/match-betting`);
  },
  UpdateMatchBetting(items) {
    return axios.patch(`${API_URL}/${userId}/match-betting`, items);
  },
};
