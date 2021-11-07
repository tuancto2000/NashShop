import React from "react";
import "./topbar.css";
import { useHistory } from "react-router-dom";
import { ExitToApp } from "@material-ui/icons";
export default function Topbar() {
  let history = useHistory();
  const handleLogoutClick = () => {
    console.log("clickme");
    localStorage.clear();
    history.push("/home");
    history.go(0);
  };

  return (
    <div className="topbar">
      <div className="topbarWrapper">
        <div className="topLeft">
          <span className="logo">NashShop</span>
        </div>
        <div className="topRight">
          {localStorage.getItem("token") && (
            <button className="buttonLogout" onClick={handleLogoutClick}>
              <ExitToApp></ExitToApp>
            </button>
          )}
        </div>
      </div>
    </div>
  );
}
