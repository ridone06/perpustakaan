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
        this.setState({
            param: {
                ...this.state.param
            }
        });
    }

    onClickSave() {
        this.state.param.PetugasId = 1;
        this.props.save(this.state.param, this.props.dataId);
    }

    render() {
        const { listPengarang = null, isLoading = false, listPenerbit = null, listRak = null, disabled = false } = this.props;
        const { param } = this.state;

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
                                                onChange={(e) => this.onInputChane("Judul", e.target.value)} />
                                            <CFormText className="help-block">Please enter judul</CFormText>
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
                                                onChange={(e) => this.onInputChane("TahunTerbit", e.target.value)} />
                                            <CFormText className="help-block">Please enter tanggal tahun terbit</CFormText>
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
                                                onChange={(e) => this.onInputChane("KodeRak", e.target.value)}>
                                                <option value="0">Please select</option>
                                                {
                                                    (listRak && listRak.length > 0) ?
                                                        listRak.map((item, index) => {
                                                            return (<option key={index} value={item.Kode}>{item.Kode}</option>)
                                                        }) : null
                                                }
                                            </CSelect>
                                            <CFormText className="help-block">Please select rak</CFormText>
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
                                                onChange={(e) => this.onInputChane("PenerbitId", e.target.value)}>
                                                <option value="0">Please select</option>
                                                {
                                                    (listPenerbit && listPenerbit.length > 0) ?
                                                        listPenerbit.map((item, index) => {
                                                            return (<option key={index} value={item.Id}>{item.Nama}</option>)
                                                        }) : null
                                                }
                                            </CSelect>
                                            <CFormText className="help-block">Please select penerbit</CFormText>
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
                                                onChange={(e) => this.onInputChane("PengarangId", e.target.value)}>
                                                <option value="0">Please select</option>
                                                {
                                                    (listPengarang && listPengarang.length > 0) ?
                                                        listPengarang.map((item, index) => {
                                                            return (<option key={index} value={item.Id}>{item.Nama}</option>)
                                                        }) : null
                                                }
                                            </CSelect>
                                            <CFormText className="help-block">Please select pengarang</CFormText>
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