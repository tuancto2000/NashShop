import React from "react";
import "./login.css";
import { useHistory } from "react-router-dom";
import { useForm } from "react-hook-form";
import { Authenticate } from "../../services/userService";
export default function Login() {
  const { register, handleSubmit } = useForm();
  let history = useHistory();
  const onHandleSubmit = (data) => {
    Authenticate(data)
      .then((response) => {
        if (response) {
          let expiresOn = new Date();
          expiresOn.setDate(expiresOn.getHours() + 1);
          const temp = {
            token: response,
            expiresOn: expiresOn,
          };
          console.log(temp);
          const stringUser = JSON.stringify(temp);
          localStorage.setItem("user", stringUser);

          history.push("/");
          history.go(0);
        }
      })
      .catch((error) => console.log(error));
    if (localStorage.getItem("user")) {
      console.log("Login succeed");
      history.push("/home");
      //history.go(0);
    }
  };
  return (
    <div className="container" onclick="onclick">
      <div className="top"></div>
      <div className="bottom"></div>
      <div className="center">
        <form onSubmit={handleSubmit(onHandleSubmit)}>
          <h2>Please Sign In</h2>
          <input
            type="text"
            placeholder="username"
            {...register("username", { required: true })}
          />
          <input
            type="password"
            placeholder="password"
            {...register("password", { required: true })}
          />
          <button type="submit">Login</button>
          <h2>&nbsp;</h2>
        </form>
      </div>
    </div>
  );
}
