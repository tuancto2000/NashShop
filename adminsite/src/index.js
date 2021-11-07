import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import axios from "axios";
const token = localStorage.getItem("token");
axios.defaults.baseURL = "https://localhost:5000";
axios.defaults.headers.common["Authorization"] = "Bearer " + token;

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById("root")
);
