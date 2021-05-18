import axios from "axios";
import { API } from "../actions/types";
import { accessDenied, apiError, apiStart, apiEnd } from "../actions/api";

const apiMiddleware = ({ dispatch }) => next => action => {
  next(action);

  if (action.type !== API) return;

  const {
    url,
    method,
    data,
    accessToken,
    onSuccess,
    onFailure,
    label,
    headers
  } = action.payload;
  const dataOrParams = ["GET", "DELETE"].includes(method) ? "params" : "data";

  // axios default configs
  const instance = axios.create({
    baseURL: process.env.BASE_API || "http://localhost:5000/api/",
  });

  instance.defaults.headers.common["Content-Type"] = "application/json";
  // instance.defaults.headers.common["Authorization"] = `Bearer ${accessToken}`;
  instance.defaults.headers.common["Authorization"] = `Bearer YWRtaW46UEBzc3cwcmQ=`;

  if (label) {
    dispatch(apiStart(label));
  }

  instance
    .request({
      url,
      method,
      headers,
      [dataOrParams]: data
    })
    .then((response) => {
      if (response.data.statusCode && response.data.statusCode === 200) {
        let data = response.data;

        if (data.result === {} || data.result.length <= 0) {
          data.result = [];
        }
        dispatch(onSuccess(data.result));
      } else if (response.status && response.status === 200) {
        let data = response.data;
        if (data === {}) {
          data = null;
        }
        
        dispatch(onSuccess(data));
      }
      else {
        dispatch(apiError(label, data.Errors));
      }
    })
    .catch(error => {
      dispatch(apiError(label, error));
      // dispatch(onFailure(error));

      if (error.response && error.response.status === 403) {
        dispatch(accessDenied(window.location.pathname));
      }
    })
    .finally(() => {
      if (label) {
        dispatch(apiEnd(label));
      }
    });
};

export default apiMiddleware;