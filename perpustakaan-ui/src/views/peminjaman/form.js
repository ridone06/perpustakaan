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
    CFormText,
    CInput,
    CLabel,
    CSelect,
    CRow
} from '@coreui/react'
import CIcon from '@coreui/icons-react';
import ListPeminjamanBuku from './listBuku';
import { getAll as getAllAnggota } from "../../actions/anggota";
import { getById as getPeminjaman, save as savePeminjaman, backToList } from "../../actions/peminjaman";
import { getAll as getAllBuku } from "../../actions/buku";

class FormPeminjaman extends Component {
    constructor(props) {
        super(props);

        this.state = {
            param: {
                TanggalPinjam: "",
                TanggalKembali: "",
                AnggotaId: 0,
                PetugasId: 1,
                Details: null
            }
        }

        this.onAddBuku = this.onAddBuku.bind(this);
        this.onDeleteBuku = this.onDeleteBuku.bind(this);
        this.onClickSave = this.onClickSave.bind(this);
    }

    componentDidMount() {
        if (!this.props.isLoading) {
            this.props.getPeminjaman(this.props.dataId || 0);
            this.props.getAllAnggota();
            this.props.getAllBuku();
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
        this.setState({
            param: {
                ...this.state.param
            }
        });
    }

    onAddBuku(detail) {
        let details = this.state.param.Details || [];
        details.push(detail);
        this.setState({
            param: {
                ...this.state.param,
                Details: details
            }
        });
    }

    onDeleteBuku(id) {
        let details = this.state.param.Details.filter((item) => {
            return item.BukuId !== id;
        });

        this.setState({
            param: {
                ...this.state.param,
                Details: details
            }
        });
    }

    onClickSave() {
        this.state.param.PetugasId = 1;
        this.props.savePeminjaman(this.state.param, this.props.dataId);
    }

    render() {
        const { listBuku = null, isLoading = false, listAnggota = null, disabled = false } = this.props;
        const { param } = this.state;

        console.log("disabled", disabled);

        return (
            <>
                <CCard>
                    <CCardHeader>
                        Peminjaman <small> Form</small>
                    </CCardHeader>
                    <CCardBody>
                        <CForm action="" method="post" className="form-horizontal">
                            <CRow>
                                <CCol xs="12" md="6">
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="hf-email">Tanggal Pinjam</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="date"
                                                id="TanggalPinjam"
                                                name="TanggalPinjam"
                                                placeholder="Tanggal Pinjam"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.TanggalPinjam}
                                                onChange={(e) => this.onInputChane("TanggalPinjam", e.target.value)} />
                                            <CFormText className="help-block">Please enter tanggal pinjam</CFormText>
                                        </CCol>
                                    </CFormGroup>
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="hf-email">Tanggal Kembali</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="date"
                                                id="TanggalKembali"
                                                name="TanggalKembali"
                                                placeholder="Tanggal Kembali"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.TanggalKembali}
                                                onChange={(e) => this.onInputChane("TanggalKembali", e.target.value)} />
                                            <CFormText className="help-block">Please enter tanggal kembali</CFormText>
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
                                                disabled={disabled}
                                                onChange={(e) => this.onInputChane("AnggotaId", e.target.value)}>
                                                <option value="0">Please select</option>
                                                {
                                                    (listAnggota && listAnggota.length > 0) ?
                                                        listAnggota.map((item, index) => {
                                                            return (<option key={index} value={item.Id}>{item.Nama}</option>)
                                                        }) : null
                                                }
                                            </CSelect>
                                            <CFormText className="help-block">Please select anggota</CFormText>
                                        </CCol>
                                    </CFormGroup>
                                </CCol>
                                <CCol xs="12" md="12">
                                    <ListPeminjamanBuku
                                        peminjamanId={this.props.dataId}
                                        canDelete={!disabled}
                                        listBuku={listBuku}
                                        data={(param != null) ? param.Details : null}
                                        onAdd={this.onAddBuku}
                                        onDelete={this.onDeleteBuku} />
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
    listBuku: state.buku.data,
    data: state.peminjaman.data,
    dataId: state.peminjaman.dataId,
    disabled: state.peminjaman.disabled,
    isLoading: state.peminjaman.isLoading || state.buku.isLoading || state.anggota.isLoading
});

export default connect(
    mapStateToProps,
    {
        getAllBuku,
        getAllAnggota,
        getPeminjaman,
        savePeminjaman,
        backToList
    }
)(FormPeminjaman);