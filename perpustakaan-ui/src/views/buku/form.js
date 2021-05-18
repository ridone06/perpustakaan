import React, { Component } from 'react'
import { connect } from "react-redux";
import {
    CButton,
    CCard,
    CCardBody,
    CCardFooter,
    CCardHeader,
    CCol,
    CForm,
    CFormGroup,
    CInput,
    CLabel,
    CSelect,
    CInvalidFeedback,
    CRow
} from '@coreui/react'
import CIcon from '@coreui/icons-react';
import { getAll as getAllPenerbit } from "../../actions/penerbit";
import { getAll as getAllPengarang } from "../../actions/pengarang";
import { getAll as getAllRak } from "../../actions/rak";
import { getById, save, backToList } from "../../actions/buku";

class FormBuku extends Component {
    constructor(props) {
        super(props);

        this.state = {
            param: {
                Judul: "",
                TahunTerbit: "",
                PengarangId: 0,
                PenerbitId: 0,
                KodeRak: ""
            },
            formValidation: {
                Judul: {
                    invalid: null,
                    message: "Please enter judul"
                },
                TahunTerbit: {
                    invalid: null,
                    message: "Please enter tahun terbit"
                },
                PengarangId: {
                    invalid: null,
                    message: "Please select pengarang"
                },
                PenerbitId: {
                    invalid: null,
                    message: "Please select penerbit"
                },
                KodeRak: {
                    invalid: null,
                    message: "Please select rak"
                }
            }
        }

        this.onClickSave = this.onClickSave.bind(this);
    }

    componentDidMount() {
        if (!this.props.isLoading) {
            this.props.getById(this.props.dataId || 0);
            this.props.getAllPenerbit();
            this.props.getAllPengarang();
            this.props.getAllRak();
        }
    }

    UNSAFE_componentWillReceiveProps(nextProps) {
        if (nextProps.data) {
            this.setState({
                param: nextProps.data
            });
        }
    }

    onInputChane(key, value) {
        this.state.param[key] = value;
        if (this.state.formValidation[key])
            this.state.formValidation[key].invalid = (this.state.param[key] || "") === "";

        this.setState({
            param: {
                ...this.state.param
            }
        });
    }

    onClickSave() {
        if (this.isValid()) {
            this.state.param.PetugasId = 1;
            this.props.save(this.state.param, this.props.dataId);
        }
    }

    isValid() {
        this.state.formValidation.Judul.invalid = (this.state.param.Judul || "") === "";
        this.state.formValidation.TahunTerbit.invalid = (this.state.param.TahunTerbit || "") === "";
        this.state.formValidation.PengarangId.invalid = (this.state.param.PengarangId || "") === "";
        this.state.formValidation.PenerbitId.invalid = (this.state.param.PenerbitId || "") === "";
        this.state.formValidation.KodeRak.invalid = (this.state.param.KodeRak || "") === "";

        this.setState({
            ...this.state.formValidation
        });

        return !this.state.formValidation.Judul.invalid
            && !this.state.formValidation.TahunTerbit.invalid
            && !this.state.formValidation.PengarangId.invalid
            && !this.state.formValidation.PenerbitId.invalid
            && !this.state.formValidation.KodeRak.invalid;;
    }

    render() {
        const { listPengarang = null, isLoading = false, listPenerbit = null, listRak = null, disabled = false } = this.props;
        const { param, formValidation } = this.state;

        return (
            <>
                <CCard>
                    <CCardHeader>
                        Buku <small> Form</small>
                    </CCardHeader>
                    <CCardBody>
                        <CForm action="" method="post" className="form-horizontal">
                            <CRow>
                                <CCol xs="12" md="6">
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="Judul">Judul</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="text"
                                                id="Judul"
                                                name="Judul"
                                                placeholder="Judul"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.Judul}
                                                onChange={(e) => this.onInputChane("Judul", e.target.value)}
                                                invalid={formValidation.Judul.invalid} />
                                            {
                                                (formValidation.Judul.invalid) ?
                                                    <CInvalidFeedback>{formValidation.Judul.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="TahunTerbit">Tahun Terbit</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="number"
                                                id="TahunTerbit"
                                                name="TahunTerbit"
                                                placeholder="Tahun Terbit"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.TahunTerbit}
                                                onChange={(e) => this.onInputChane("TahunTerbit", e.target.value)}
                                                invalid={formValidation.TahunTerbit.invalid} />
                                            {
                                                (formValidation.TahunTerbit.invalid) ?
                                                    <CInvalidFeedback>{formValidation.TahunTerbit.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="KodeRak">Rak</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CSelect custom name="KodeRak"
                                                id="KodeRak"
                                                value={param.KodeRak}
                                                disabled={disabled}
                                                invalid={formValidation.KodeRak.invalid}
                                                onChange={(e) => this.onInputChane("KodeRak", e.target.value)}>
                                                <option value="">Please select</option>
                                                {
                                                    (listRak && listRak.length > 0) ?
                                                        listRak.map((item, index) => {
                                                            return (<option key={index} value={item.Kode}>{item.Kode}</option>)
                                                        }) : null
                                                }
                                            </CSelect>
                                            {
                                                (formValidation.KodeRak.invalid) ?
                                                    <CInvalidFeedback>{formValidation.KodeRak.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
                                </CCol>
                                <CCol xs="12" md="6">
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="PenerbitId">Penerbit</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CSelect custom name="PenerbitId"
                                                id="PenerbitId"
                                                value={param.PenerbitId}
                                                disabled={disabled}
                                                invalid={formValidation.PenerbitId.invalid}
                                                onChange={(e) => this.onInputChane("PenerbitId", e.target.value)}>
                                                <option value="">Please select</option>
                                                {
                                                    (listPenerbit && listPenerbit.length > 0) ?
                                                        listPenerbit.map((item, index) => {
                                                            return (<option key={index} value={item.Id}>{item.Nama}</option>)
                                                        }) : null
                                                }
                                            </CSelect>
                                            {
                                                (formValidation.PenerbitId.invalid) ?
                                                    <CInvalidFeedback>{formValidation.PenerbitId.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="PengarangId">Pengarang</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CSelect custom name="PengarangId"
                                                id="PengarangId"
                                                value={param.PengarangId}
                                                disabled={disabled}
                                                invalid={formValidation.PengarangId.invalid}
                                                onChange={(e) => this.onInputChane("PengarangId", e.target.value)}>
                                                <option value="">Please select</option>
                                                {
                                                    (listPengarang && listPengarang.length > 0) ?
                                                        listPengarang.map((item, index) => {
                                                            return (<option key={index} value={item.Id}>{item.Nama}</option>)
                                                        }) : null
                                                }
                                            </CSelect>
                                            {
                                                (formValidation.PengarangId.invalid) ?
                                                    <CInvalidFeedback>{formValidation.PengarangId.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
                                </CCol>
                            </CRow>
                        </CForm>
                    </CCardBody>
                    <CCardFooter>
                        <CButton hidden={disabled} type="submit" size="sm" color="primary" onClick={this.onClickSave}><CIcon name="cil-save" /> Save</CButton>
                        &nbsp;
                        <CButton type="reset" size="sm" color="warning" onClick={() => {
                            this.props.backToList();
                        }}><CIcon name="cil-chevron-left" /> Back</CButton>
                    </CCardFooter>
                </CCard>
            </>
        );
    }
}

const mapStateToProps = (state) => ({
    listPenerbit: state.penerbit.data,
    listPengarang: state.pengarang.data,
    listRak: state.rak.data,
    data: state.buku.data,
    dataId: state.buku.dataId,
    disabled: state.buku.disabled,
    isLoading: state.buku.isLoading || state.penerbit.isLoading || state.pengarang.isLoading
});

export default connect(
    mapStateToProps,
    {
        getAllPenerbit,
        getAllPengarang,
        getAllRak,
        getById,
        save,
        backToList
    }
)(FormBuku);