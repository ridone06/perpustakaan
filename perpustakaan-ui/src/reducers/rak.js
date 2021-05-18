import {
    GET_DATA_RAK,
    SET_DATA_RAK,
    GET_LIST_RAK,
    SET_LIST_RAK,
    SET_ADD_RAK,
    SET_EDIT_RAK,
    SAVE_RAK,
    DELETE_RAK,
    BACK_TO_LIST_RAK,
    API_START,
    API_END,
    API_ERROR
} from "../actions/types";

const initialState = {
    sidebarShow: 'responsive',
}

export default function rakReducer(state = initialState, { type, payload, pageMode, kode, disabled, error, ...rest }) {
    switch (type) {
        case 'set':
            return { ...state, ...rest };
        case SET_DATA_RAK:
        case SET_LIST_RAK:
        case BACK_TO_LIST_RAK:
            return {
                ...state,
                data: payload,
                pageMode: pageMode
            };
        case SET_ADD_RAK:
        case SET_EDIT_RAK:
            return {
                data: payload,
                pageMode: pageMode,
                kode: kode || "",
                disabled: disabled || false
            };
        case API_START:
            {
                switch (payload) {
                    case GET_DATA_RAK:
                    case GET_LIST_RAK:
                    case SAVE_RAK:
                    case DELETE_RAK:
                        return {
                            ...state,
                            isLoading: true
                        };
                    default:
                        return state;
                }
            }
        case API_ERROR:
        case API_END:
            {
                switch (payload) {
                    case GET_DATA_RAK:
                    case GET_LIST_RAK:
                    case SAVE_RAK:
                    case DELETE_RAK:
                        return {
                            ...state,
                            error: error,
                            isLoading: false
                        };
                    default:
                        return state;
                }
            }
        default:
            return state;
    }
}