import { createStore, applyMiddleware } from "redux";
import rootReducer from "./reducers";
import apiMiddleware from "./middleware/api";
import { composeWithDevTools } from 'redux-devtools-extension';

const store = createStore(rootReducer, composeWithDevTools(
  applyMiddleware(apiMiddleware))
);
// window.store = store;
export default store;