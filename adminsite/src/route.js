import React, { useContext } from "react";
import { Route } from "react-router-dom";
import Login from "./components/login/Login";

export const PrivateRoute = ({ component: Component, ...rest }) => {
  const user = JSON.parse(localStorage.getItem("user"));
  const token = user ? user["token"] : "";
  const expriresOn = user ? new Date(user["expiresOn"]) : "";
  const isAuth = token && expriresOn > new Date() ? true : false;

  if (!isAuth) {
    localStorage.clear();
    return <Login />;
  } else {
    return (
      <Route
        {...rest}
        render={(props) => (isAuth ? <Component {...props} /> : <Login />)}
      />
    );
  }
};
