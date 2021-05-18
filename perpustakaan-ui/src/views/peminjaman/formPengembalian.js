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
import ListPeminjamanBuku from './listBuku';
import { getAll as getAllAnggota } from "../../actions/anggota";
import { getById } from "../../actions/pengembalian";
import { backToList, savePengembalian } from "../../actions/peminjaman";

class FormPengembalian extends Component {
    constructor(props) {
        super(props);

        this.state = {
            param: {
                PeminjamanId: this.props.peminjaman.data.Id,
                TanggalPengembalian: "",
                Denda: 0,
                AnggotaId: this.props.peminjaman.data.AnggotaId,
                PetugasId: 1,
                Details: this.props.peminjaman.data.Details
            },
            formValidation: {
                TanggalPengembalian: {
                    invalid: null,
                    message: "Please enter tanggal pengembalian"
                }
            }
        }

        this.onClickSave = this.onClickSave.bind(this);
    }


    UNSAFE_componentWillReceiveProps(nextProps) {
        if (nextProps.data) {
            this.state.param.TanggalPengembalian = nextProps.data.TanggalPengembalian;
            this.state.param.Denda = nextProps.data.Denda;
            this.setState({
                ...this.state.param
            });
        }
    }

    componentDidMount() {
        if (!this.props.isLoading) {
            this.props.getById(this.state.param.PeminjamanId);
            this.props.getAllAnggota();
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
            this.props.savePengembalian(this.state.param, 0);
        }
    }

    isValid() {
        this.state.formValidation.TanggalPengembalian.invalid = (this.state.param.TanggalPengembalian || "") === "";

        this.setState({
            ...this.state.formValidation
        });

        return !this.state.formValidation.TanggalPengembalian.invalid;
    }

    render() {
        const { isLoading = false, listAnggota = null, disabled = false } = this.props;
        const { param, formValidation } = this.state;

        return (
            <>
                <CCard>
                    <CCardHeader>
                        Pengembalian <small> Form</small>
                    </CCardHeader>
                    <CCardBody>
                        <CForm action="" method="post" className="form-horizontal">
                            <CRow>
                                <CCol xs="12" md="6">
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="TanggalPengembalian">Tanggal Pengembalian</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="date"
                                                id="TanggalPengembalian"
                                                name="TanggalPengembalian"
                                                placeholder="Tanggal Pengembalian"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.TanggalPengembalian}
                                                onChange={(e) => this.onInputChane("TanggalPengembalian", e.target.value)}
                                                invalid={formValidation.TanggalPengembalian.invalid} />
                                            {
                                                (formValidation.TanggalPengembalian.invalid) ?
                                                    <CInvalidFeedback>{formValidation.TanggalPengembalian.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="Denda">Denda</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="number"
                                                id="Denda"
                                                name="Denda"
                                                placeholder="Denda"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.Denda}
                                                onChange={(e) => this.onInputChane("Denda", e.target.value)} />
                                        </CCol>
                                    </CFormGroup>
                                </CCol>
                                <CCol xs="12" md="6">
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="hf-email">Anggota</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CSelect custom name="select"
                                                id="select"
                                                value={param.AnggotaId}
                                                disabled={true}
                                                onChange={(e) => this.onInputChane("AnggotaId", e.target.value)}>
                                                <option value="0">Please select</option>
                                                {
                                                    (listAnggota && listAnggota.length > 0) ?
                                                        listAnggota.map((item, index) => {
                                                            return (<option key={index} value={item.Id}>{item.Nama}</option>)
                                                        }) : null
                                                }
                                            </CSelect>
                                        </CCol>
                                    </CFormGroup>
                                </CCol>
                                <CCol xs="12" md="12">
                                    <ListPeminjamanBuku
                                        peminjamanId={this.props.peminjaman.Id}
                                        canDelete={false}
                                        listBuku={null}
                                        data={(param != null) ? param.Details : null} />
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
    listAnggota: state.anggota.data,
    peminjaman: state.peminjaman,
    data: state.pengembalian.data,
    disabled: state.peminjaman.disabled,
    isLoading: state.pengembalian.isLoading || state.buku.isLoading || state.anggota.isLoading
});

export default connect(
    mapStateToProps,
    {
        getAllAnggota,
        getById,
        savePengembalian,
        backToList
    }
)(FormPengembalian);