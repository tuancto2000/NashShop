import React, { useContext } from "react";
import { Route } from "react-router-dom";

import Login from "./components/login/Login";

export const PrivateRoute = ({ component: Component, ...rest }) => {
  const isAuth = localStorage.getItem("token");
  if (!isAuth) return <Login />;
  else {
    return (
      <Route
        {...rest}
        render={(props) => (isAuth ? <Component {...props} /> : <Login />)}
      />
    );
  }
};
