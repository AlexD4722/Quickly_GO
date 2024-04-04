import React from "react";
import ReactDOM from "react-dom/client";
import "./styles/index.scss";
import reportWebVitals from "./reportWebVitals";
import "./styles/index.scss";
import store from "./state/store/configureStore";
import { Provider } from "react-redux";
import "flowbite";
import { RouterProvider } from "react-router-dom";
import router from "./router/router";
import AlertMessage from "./components/AlertMessage";
import { PersistGate } from "redux-persist/integration/react";
import { persistor } from "./state/store/configureStore";
const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <Provider store={store}>
    <PersistGate loading={null} persistor={persistor}>
      <React.StrictMode>
        <div className="wrapper">
          <AlertMessage />
          <RouterProvider router={router} />
        </div>
      </React.StrictMode>
    </PersistGate>
  </Provider>
);
reportWebVitals();
